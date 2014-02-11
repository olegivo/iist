using System;
using System.Configuration;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Configuration
{
    ///<summary>
    ///
    ///</summary>
    public class LoadOptionsCollection : ConfigurationElementCollection
    {
        ///<summary>
        ///
        ///</summary>
        public LoadOptionsCollection()
        {
            //var loadOptionsConfigElement = (LoadOptionsConfigElement)CreateNewElement();
            //// Add the element to the collection.
            //Add(loadOptionsConfigElement);
        }

        ///<summary>
        ///Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection"></see>.
        ///</summary>
        ///
        ///<returns>
        ///The <see cref="T:System.Configuration.ConfigurationElementCollectionType"></see> of this collection.
        ///</returns>
        ///
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        ///<summary>
        ///When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A new <see cref="T:System.Configuration.ConfigurationElement"></see>.
        ///</returns>
        ///
        protected override ConfigurationElement CreateNewElement()
        {
            return new LoadOptionsConfigElement();
        }


        ///<summary>
        ///Creates a new <see cref="T:System.Configuration.ConfigurationElement"></see> when overridden in a derived class.
        ///</summary>
        ///
        ///<returns>
        ///A new <see cref="T:System.Configuration.ConfigurationElement"></see>.
        ///</returns>
        ///
        ///<param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement"></see> to create. </param>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new LoadOptionsConfigElement(elementName);
        }


        ///<summary>
        ///Gets the element key for a specified configuration element when overridden in a derived class.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Object"></see> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"></see>.
        ///</returns>
        ///
        ///<param name="element">The <see cref="T:System.Configuration.ConfigurationElement"></see> to return the key for. </param>
        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((LoadOptionsConfigElement)element).FieldBusType/*Name*/;
        }


        ///<summary>
        ///
        ///</summary>
        public new string AddElementName
        {
            get { return base.AddElementName; }
            set { base.AddElementName = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public new string ClearElementName
        {
            get { return base.ClearElementName; }
            set { base.AddElementName = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public new string RemoveElementName
        {
            get { return base.RemoveElementName; }
        }

        ///<summary>
        ///
        ///</summary>
        public new int Count
        {
            get { return base.Count; }
        }


        ///<summary>
        ///
        ///</summary>
        ///<param name="index"></param>
        public LoadOptionsConfigElement this[int index]
        {
            get { return (LoadOptionsConfigElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusType"></param>
        public new LoadOptionsConfigElement this[FieldBusType fieldBusType]
        {
            get { return (LoadOptionsConfigElement)BaseGet(fieldBusType); }
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="Name"></param>
        public new LoadOptionsConfigElement this[string Name]
        {
            get { return (LoadOptionsConfigElement)BaseGet(Name); }
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="loadOptionsConfigElement"></param>
        ///<returns></returns>
        public int IndexOf(LoadOptionsConfigElement loadOptionsConfigElement)
        {
            return BaseIndexOf(loadOptionsConfigElement);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="loadOptionsConfigElement"></param>
        public void Add(LoadOptionsConfigElement loadOptionsConfigElement)
        {
            BaseAdd(loadOptionsConfigElement);

            // Add custom code here.
        }

        ///<summary>
        ///Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection"></see>.
        ///</summary>
        ///
        ///<param name="element">The <see cref="T:System.Configuration.ConfigurationElement"></see> to add. </param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="loadOptionsConfigElement"></param>
        public void Remove(LoadOptionsConfigElement loadOptionsConfigElement)
        {
            if (BaseIndexOf(loadOptionsConfigElement) >= 0)
                BaseRemove(loadOptionsConfigElement.FieldBusType/*Name*/);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="index"></param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="name"></param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        ///<summary>
        ///
        ///</summary>
        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }

    }
}