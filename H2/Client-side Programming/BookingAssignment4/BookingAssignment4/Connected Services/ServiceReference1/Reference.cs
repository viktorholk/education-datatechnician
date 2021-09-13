﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookingAssignment4.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BookingItem", Namespace="http://schemas.datacontract.org/2004/07/WCF_BOOKING")]
    [System.SerializableAttribute()]
    public partial class BookingItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExtraField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int XField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int YField;
        
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
        public string Extra {
            get {
                return this.ExtraField;
            }
            set {
                if ((object.ReferenceEquals(this.ExtraField, value) != true)) {
                    this.ExtraField = value;
                    this.RaisePropertyChanged("Extra");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int State {
            get {
                return this.StateField;
            }
            set {
                if ((this.StateField.Equals(value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int X {
            get {
                return this.XField;
            }
            set {
                if ((this.XField.Equals(value) != true)) {
                    this.XField = value;
                    this.RaisePropertyChanged("X");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Y {
            get {
                return this.YField;
            }
            set {
                if ((this.YField.Equals(value) != true)) {
                    this.YField = value;
                    this.RaisePropertyChanged("Y");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetBookingDescription", ReplyAction="http://tempuri.org/IService1/GetBookingDescriptionResponse")]
        string GetBookingDescription();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetBookingDescription", ReplyAction="http://tempuri.org/IService1/GetBookingDescriptionResponse")]
        System.Threading.Tasks.Task<string> GetBookingDescriptionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateBookingNumber", ReplyAction="http://tempuri.org/IService1/CreateBookingNumberResponse")]
        bool CreateBookingNumber(int bookingNumber, int numberOfBookingItems);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateBookingNumber", ReplyAction="http://tempuri.org/IService1/CreateBookingNumberResponse")]
        System.Threading.Tasks.Task<bool> CreateBookingNumberAsync(int bookingNumber, int numberOfBookingItems);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteBookingNumber", ReplyAction="http://tempuri.org/IService1/DeleteBookingNumberResponse")]
        bool DeleteBookingNumber(int bookingNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteBookingNumber", ReplyAction="http://tempuri.org/IService1/DeleteBookingNumberResponse")]
        System.Threading.Tasks.Task<bool> DeleteBookingNumberAsync(int bookingNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetBookingItems", ReplyAction="http://tempuri.org/IService1/GetBookingItemsResponse")]
        BookingAssignment4.ServiceReference1.BookingItem[] GetBookingItems(int bookingNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetBookingItems", ReplyAction="http://tempuri.org/IService1/GetBookingItemsResponse")]
        System.Threading.Tasks.Task<BookingAssignment4.ServiceReference1.BookingItem[]> GetBookingItemsAsync(int bookingNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetBookingItems", ReplyAction="http://tempuri.org/IService1/SetBookingItemsResponse")]
        bool SetBookingItems(int bookingNumber, BookingAssignment4.ServiceReference1.BookingItem[] bookings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetBookingItems", ReplyAction="http://tempuri.org/IService1/SetBookingItemsResponse")]
        System.Threading.Tasks.Task<bool> SetBookingItemsAsync(int bookingNumber, BookingAssignment4.ServiceReference1.BookingItem[] bookings);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : BookingAssignment4.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<BookingAssignment4.ServiceReference1.IService1>, BookingAssignment4.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetBookingDescription() {
            return base.Channel.GetBookingDescription();
        }
        
        public System.Threading.Tasks.Task<string> GetBookingDescriptionAsync() {
            return base.Channel.GetBookingDescriptionAsync();
        }
        
        public bool CreateBookingNumber(int bookingNumber, int numberOfBookingItems) {
            return base.Channel.CreateBookingNumber(bookingNumber, numberOfBookingItems);
        }
        
        public System.Threading.Tasks.Task<bool> CreateBookingNumberAsync(int bookingNumber, int numberOfBookingItems) {
            return base.Channel.CreateBookingNumberAsync(bookingNumber, numberOfBookingItems);
        }
        
        public bool DeleteBookingNumber(int bookingNumber) {
            return base.Channel.DeleteBookingNumber(bookingNumber);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteBookingNumberAsync(int bookingNumber) {
            return base.Channel.DeleteBookingNumberAsync(bookingNumber);
        }
        
        public BookingAssignment4.ServiceReference1.BookingItem[] GetBookingItems(int bookingNumber) {
            return base.Channel.GetBookingItems(bookingNumber);
        }
        
        public System.Threading.Tasks.Task<BookingAssignment4.ServiceReference1.BookingItem[]> GetBookingItemsAsync(int bookingNumber) {
            return base.Channel.GetBookingItemsAsync(bookingNumber);
        }
        
        public bool SetBookingItems(int bookingNumber, BookingAssignment4.ServiceReference1.BookingItem[] bookings) {
            return base.Channel.SetBookingItems(bookingNumber, bookings);
        }
        
        public System.Threading.Tasks.Task<bool> SetBookingItemsAsync(int bookingNumber, BookingAssignment4.ServiceReference1.BookingItem[] bookings) {
            return base.Channel.SetBookingItemsAsync(bookingNumber, bookings);
        }
    }
}
