using System;
using System.Linq;
using System.Collections.Generic;
using AptitudeEngine.Events;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Enums;

namespace AptitudeEngine
{
    public class AptObject : ComponentEventChain, IDisposable
    {
        private HashSet<AptComponent> components;
        private List<AptObject> children;
        private AptObject parent;
        private List<AptComponent> compsToStart = new List<AptComponent>();

        public string Guid { get; }

        public string Name { get; set; }
        public AptObject Parent => parent;
        public AptObject[] Children => children.ToArray();
        public AptComponent[] Components => components.ToArray();
        public AptContext Context { get; }
        public Transform Transform { get; }
        public bool Disposed { get; private set; }

        internal AptObject(AptContext context)
        {
            // this should almost certainly run once, but GUIDs arent **guaranteed** to be unique,
            // theyre astronomically likely to be unique.
            do
            {
                // get a 32 digit GUID for this object.
                Guid = System.Guid.NewGuid().ToString("N");
            } while (context.objectTable.ContainsKey(Guid));

            context.objectTable.Add(Guid, this);

            children = new List<AptObject>();
            components = new HashSet<AptComponent>();
            Transform = new Transform();
            Context = context;
        }

        public override bool Equals(object obj)
            => obj is AptObject go && Guid.Equals(go.Guid);

        public override int GetHashCode()
        {
            // 269 and 47 are primes
            var hash = 269;
            hash = (hash * 47) + Guid.GetHashCode();
            return hash;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Disposed)
            {
                if (disposing)
                {
                    if (children?.Count > 0)
                    {
                        // We want to destroy all of the children since they belong
                        // to this object and will cease to exist after this object's destruction.
                        foreach (var child in children)
                        {
                            child.Dispose();
                        }

                        children.Clear();
                    }

                    if (components?.Count > 0)
                    {
                        foreach (var comp in components)
                        {
                            comp.Dispose();
                        }

                        components.Clear();
                    }
                }

                Name = null;
                children = null;
                components = null;
                parent = null;
            }

            Disposed = true;
        }

        public AptObject GetObjectInChildren(params string[] objectPath)
        {
            if (objectPath.Length > 1)
            {
                var items = new List<string>(objectPath);
                items.RemoveAt(0);
                foreach (var g in Children)
                {
                    if (g.Name == objectPath[0])
                    {
                        g.GetObjectInChildren(items.ToArray());
                    }
                }
            }
            else
            {
                foreach (var g in Children)
                {
                    if (g.Name == objectPath[0])
                    {
                        return g;
                    }
                }
            }

            return null;
        }

        private void IterateComponents(Action<AptComponent> action) => IterateComponents(components.ToArray(), action);

        private void IterateComponents(AptComponent[] comps, Action<AptComponent> action)
        {
            foreach (var comp in comps)
            {
                action(comp);
            }
        }

        /// <summary>
        /// Sets the parent to <paramref name="ao"/> and then tells <paramref name="ao"/> to add
        /// this object to its list of children.
        /// </summary>
        /// <param name="ao">The object to set as parent</param>
        public void SetParent(AptObject ao)
        {
            SetParentFinal(ao);
            ao?.AddChildFinal(this);
        }

        private void SetParentFinal(AptObject ao)
        {
            parent?.RemoveChildFinal(this);
            parent = ao;
        }

        /// <summary>
        /// Adds <paramref name="ao"/> to this objects list of children and then tells <paramref name="ao"/>
        /// to set this object as its parent.
        /// </summary>
        /// <param name="ao">The object to add as a child</param>
        public void AddChild(AptObject ao)
        {
            AddChildFinal(ao);
            ao?.SetParentFinal(this);
        }

        private void AddChildFinal(AptObject ao) => children.Add(ao);

        public void RemoveChild(int index)
        {
            if (children.Count < index - 1)
            {
                return;
            }

            RemoveChild(Children[index]);
        }

        /// <summary>
        /// Removes <paramref name="ao"/> from this objects list of children and ensures that <paramref name="ao"/>
        /// loses its parental reference, followed by moving it to the game hierarchy's root.
        /// </summary>
        /// <param name="ao">The child object to remove</param>
        public void RemoveChild(AptObject ao)
        {
            if (ao == null)
            {
                return;
            }

            if (RemoveChildFinal(ao))
            {
                ao.SetParentFinal(null);
                Context.AddToHierarchyRoot(ao);
            }
        }

        private bool RemoveChildFinal(AptObject ao)
            => children.Remove(ao);

        public T AddComponent<T>() where T : AptComponent, new()
        {
            var comp = new T();
            compsToStart.Add(comp);
            components.Add(comp);
            comp.owner = this;
            comp.InternalAwake();
            return comp;
        }

        public bool TryRemoveComponent(AptComponent component)
            => components.Remove(component);

        public T GetComponentOfType<T>() where T : AptComponent, new()
            => components.First(comp => comp is T) as T;

        public T[] GetComponentsOfType<T>() where T : AptComponent, new()
            => components.Where(comp => comp is T).ToArray() as T[];

        public override void Awake()
        =>
            IterateComponents(component =>
            {
                if (!component.Awoken)
                {
                    component.InternalAwake();
                }
            });

        public override void Start()
        =>
            IterateComponents(component =>
            {
                if (!component.Started && component.Awoken)
                {
                    component.InternalStart();
                }
            });

        public override void PreUpdate()
        {
            IterateComponents(compsToStart.ToArray(), comp => { comp.InternalStart(); });

            IterateComponents(component =>
            {
                if (component.Started)
                {
                    component.InternalPreUpdate();
                }
            });
        }

        public override void Update()
        =>
            IterateComponents(component =>
            {
                if (component.Started)
                {
                    component.InternalUpdate();
                }
            });

        public override void PostUpdate()
        =>
            IterateComponents(component =>
            {
                if (component.Started)
                {
                    component.InternalPostUpdate();
                }
            });

        public override void PreRender(FrameEventArgs a)
        =>
            IterateComponents(component =>
            {
                if (component.Started)
                {
                    component.InternalPreRender(a);
                }
            });

        public override void Render(FrameEventArgs a)
        =>
            IterateComponents(component =>
            {
                if (component.Started)
                {
                    component.InternalRender(a);
                }
            });

        public override void PostRender(FrameEventArgs a)
        =>
            IterateComponents(component =>
            {
                if (component.Started)
                {
                    component.InternalPostRender(a);
                }
            });

        public override void MouseDown(InputCode mouseCode)
        =>
            IterateComponents(component =>
            {
                if (component.Started)
                {
                    component.InternalMouseDown(mouseCode);
                }
            });

        public override void MouseUp(InputCode mouseCode)
        =>
            IterateComponents(component =>
            {
                if (component.Started)
                {
                    component.InternalMouseUp(mouseCode);
                }
            });

        public override void MouseClick(InputCode mouseCode)
        =>
            IterateComponents(component =>
            {
                if (component.Started)
                {
                    component.InternalMouseClick(mouseCode);
                }
            });
    }
}