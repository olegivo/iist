<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="b45cc6d5-482e-4fe5-a944-edd8bc735abf" namespace="Oleg_ivo.WAGO.Configuration" xmlSchemaNamespace="urn:Oleg_ivo.WAGO.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="DMISConfigurationSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="dMISConfigurationSection">
      <elementProperties>
        <elementProperty name="DMISConfigurations" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="dMISConfigurations" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/DMISConfigurationElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="DMISConfigurationElementCollection" xmlItemName="dMISConfig" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods" addItemName="DMISConfig" removeItemName="" clearItemsName="">
      <itemType>
        <configurationElementMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/DMISConfigurationElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="DMISConfigurationElement">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsEmulationMode" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="isEmulationMode" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="LoadOptions" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="loadOptions" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/LoadOptionsConfigurationElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="LoadOptionsConfigurationElementCollection" xmlItemName="loadOptionsConfigurationElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods" addItemName="LoadOptionLevel" removeItemName="" clearItemsName="">
      <itemType>
        <configurationElementMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/LoadOptionsConfigurationElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="LoadOptionsConfigurationElement">
      <attributeProperties>
        <attributeProperty name="FieldBusType" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="fieldBusType" isReadOnly="false" defaultValue="&quot;Unknown&quot;">
          <type>
            <externalTypeMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="FieldNodesLevel" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="fieldNodesLevel" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/LevelLoadOptionsConfigElement" />
          </type>
        </elementProperty>
        <elementProperty name="PhysicalChannelsLevel" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="physicalChannelsLevel" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/LevelLoadOptionsConfigElement" />
          </type>
        </elementProperty>
        <elementProperty name="LogicalChannelsLevel" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="logicalChannelsLevel" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/LevelLoadOptionsConfigElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="LevelLoadOptionsConfigElement">
      <attributeProperties>
        <attributeProperty name="LoadSavedConfiguration" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="loadSavedConfiguration" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="ComputeCurrentConfiguration" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="computeCurrentConfiguration" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b45cc6d5-482e-4fe5-a944-edd8bc735abf/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>