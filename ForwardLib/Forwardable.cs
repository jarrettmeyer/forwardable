using System;
using System.Dynamic;

namespace ForwardLib
{
    public class Forwardable<T> : DynamicObject
        where T : class
    {
        private readonly dynamic instance;

        protected Forwardable(dynamic instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");
            this.instance = instance;
        }

        public virtual T ForwardedObject
        {
            get { return instance; }
        }

        public virtual Type ForwardedType
        {
            get { return typeof(T); }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            try
            {
                var memberName = binder.Name;
                var propertyInfo = ForwardedType.GetProperty(memberName);
                result = propertyInfo.GetValue(instance, null);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}
