﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersoneManagement.Web.PersonService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PersonDTO", Namespace="http://schemas.datacontract.org/2004/07/AdventureWorks.Model")]
    [System.SerializableAttribute()]
    public partial class PersonDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BusinessEntityIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int EmailPromotionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FullNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MiddleNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ModifiedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool NameStyleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PersonTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SuffixField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid rowguidField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int BusinessEntityID {
            get {
                return this.BusinessEntityIDField;
            }
            set {
                if ((this.BusinessEntityIDField.Equals(value) != true)) {
                    this.BusinessEntityIDField = value;
                    this.RaisePropertyChanged("BusinessEntityID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EmailPromotion {
            get {
                return this.EmailPromotionField;
            }
            set {
                if ((this.EmailPromotionField.Equals(value) != true)) {
                    this.EmailPromotionField = value;
                    this.RaisePropertyChanged("EmailPromotion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FullName {
            get {
                return this.FullNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FullNameField, value) != true)) {
                    this.FullNameField = value;
                    this.RaisePropertyChanged("FullName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MiddleName {
            get {
                return this.MiddleNameField;
            }
            set {
                if ((object.ReferenceEquals(this.MiddleNameField, value) != true)) {
                    this.MiddleNameField = value;
                    this.RaisePropertyChanged("MiddleName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ModifiedDate {
            get {
                return this.ModifiedDateField;
            }
            set {
                if ((this.ModifiedDateField.Equals(value) != true)) {
                    this.ModifiedDateField = value;
                    this.RaisePropertyChanged("ModifiedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool NameStyle {
            get {
                return this.NameStyleField;
            }
            set {
                if ((this.NameStyleField.Equals(value) != true)) {
                    this.NameStyleField = value;
                    this.RaisePropertyChanged("NameStyle");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PersonType {
            get {
                return this.PersonTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.PersonTypeField, value) != true)) {
                    this.PersonTypeField = value;
                    this.RaisePropertyChanged("PersonType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Suffix {
            get {
                return this.SuffixField;
            }
            set {
                if ((object.ReferenceEquals(this.SuffixField, value) != true)) {
                    this.SuffixField = value;
                    this.RaisePropertyChanged("Suffix");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid rowguid {
            get {
                return this.rowguidField;
            }
            set {
                if ((this.rowguidField.Equals(value) != true)) {
                    this.rowguidField = value;
                    this.RaisePropertyChanged("rowguid");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PersonService.IPersonService")]
    public interface IPersonService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetPersonLists", ReplyAction="http://tempuri.org/IPersonService/GetPersonListsResponse")]
        PersoneManagement.Web.PersonService.PersonDTO[] GetPersonLists();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetPersonLists", ReplyAction="http://tempuri.org/IPersonService/GetPersonListsResponse")]
        System.Threading.Tasks.Task<PersoneManagement.Web.PersonService.PersonDTO[]> GetPersonListsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetPerson", ReplyAction="http://tempuri.org/IPersonService/GetPersonResponse")]
        PersoneManagement.Web.PersonService.PersonDTO GetPerson(int businessEntityId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetPerson", ReplyAction="http://tempuri.org/IPersonService/GetPersonResponse")]
        System.Threading.Tasks.Task<PersoneManagement.Web.PersonService.PersonDTO> GetPersonAsync(int businessEntityId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/CreatePerson", ReplyAction="http://tempuri.org/IPersonService/CreatePersonResponse")]
        void CreatePerson(PersoneManagement.Web.PersonService.PersonDTO personDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/CreatePerson", ReplyAction="http://tempuri.org/IPersonService/CreatePersonResponse")]
        System.Threading.Tasks.Task CreatePersonAsync(PersoneManagement.Web.PersonService.PersonDTO personDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/UpdatePerson", ReplyAction="http://tempuri.org/IPersonService/UpdatePersonResponse")]
        void UpdatePerson(PersoneManagement.Web.PersonService.PersonDTO personDTO, System.Guid oldGuild);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/UpdatePerson", ReplyAction="http://tempuri.org/IPersonService/UpdatePersonResponse")]
        System.Threading.Tasks.Task UpdatePersonAsync(PersoneManagement.Web.PersonService.PersonDTO personDTO, System.Guid oldGuild);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/DeletePerson", ReplyAction="http://tempuri.org/IPersonService/DeletePersonResponse")]
        void DeletePerson(int businessEntityId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/DeletePerson", ReplyAction="http://tempuri.org/IPersonService/DeletePersonResponse")]
        System.Threading.Tasks.Task DeletePersonAsync(int businessEntityId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetFullName", ReplyAction="http://tempuri.org/IPersonService/GetFullNameResponse")]
        string GetFullName(int businessEntityId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetFullName", ReplyAction="http://tempuri.org/IPersonService/GetFullNameResponse")]
        System.Threading.Tasks.Task<string> GetFullNameAsync(int businessEntityId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPersonServiceChannel : PersoneManagement.Web.PersonService.IPersonService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PersonServiceClient : System.ServiceModel.ClientBase<PersoneManagement.Web.PersonService.IPersonService>, PersoneManagement.Web.PersonService.IPersonService {
        
        public PersonServiceClient() {
        }
        
        public PersonServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PersonServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PersoneManagement.Web.PersonService.PersonDTO[] GetPersonLists() {
            return base.Channel.GetPersonLists();
        }
        
        public System.Threading.Tasks.Task<PersoneManagement.Web.PersonService.PersonDTO[]> GetPersonListsAsync() {
            return base.Channel.GetPersonListsAsync();
        }
        
        public PersoneManagement.Web.PersonService.PersonDTO GetPerson(int businessEntityId) {
            return base.Channel.GetPerson(businessEntityId);
        }
        
        public System.Threading.Tasks.Task<PersoneManagement.Web.PersonService.PersonDTO> GetPersonAsync(int businessEntityId) {
            return base.Channel.GetPersonAsync(businessEntityId);
        }
        
        public void CreatePerson(PersoneManagement.Web.PersonService.PersonDTO personDTO) {
            base.Channel.CreatePerson(personDTO);
        }
        
        public System.Threading.Tasks.Task CreatePersonAsync(PersoneManagement.Web.PersonService.PersonDTO personDTO) {
            return base.Channel.CreatePersonAsync(personDTO);
        }
        
        public void UpdatePerson(PersoneManagement.Web.PersonService.PersonDTO personDTO, System.Guid oldGuild) {
            base.Channel.UpdatePerson(personDTO, oldGuild);
        }
        
        public System.Threading.Tasks.Task UpdatePersonAsync(PersoneManagement.Web.PersonService.PersonDTO personDTO, System.Guid oldGuild) {
            return base.Channel.UpdatePersonAsync(personDTO, oldGuild);
        }
        
        public void DeletePerson(int businessEntityId) {
            base.Channel.DeletePerson(businessEntityId);
        }
        
        public System.Threading.Tasks.Task DeletePersonAsync(int businessEntityId) {
            return base.Channel.DeletePersonAsync(businessEntityId);
        }
        
        public string GetFullName(int businessEntityId) {
            return base.Channel.GetFullName(businessEntityId);
        }
        
        public System.Threading.Tasks.Task<string> GetFullNameAsync(int businessEntityId) {
            return base.Channel.GetFullNameAsync(businessEntityId);
        }
    }
}
