﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherWSLab2.ServiceTemperature {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.webserviceX.NET/", ConfigurationName="ServiceTemperature.ConvertTemperatureSoap")]
    public interface ConvertTemperatureSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.webserviceX.NET/ConvertTemp", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        double ConvertTemp(double Temperature, WeatherWSLab2.ServiceTemperature.TemperatureUnit FromUnit, WeatherWSLab2.ServiceTemperature.TemperatureUnit ToUnit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.webserviceX.NET/ConvertTemp", ReplyAction="*")]
        System.Threading.Tasks.Task<double> ConvertTempAsync(double Temperature, WeatherWSLab2.ServiceTemperature.TemperatureUnit FromUnit, WeatherWSLab2.ServiceTemperature.TemperatureUnit ToUnit);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.webserviceX.NET/")]
    public enum TemperatureUnit {
        
        /// <remarks/>
        degreeCelsius,
        
        /// <remarks/>
        degreeFahrenheit,
        
        /// <remarks/>
        degreeRankine,
        
        /// <remarks/>
        degreeReaumur,
        
        /// <remarks/>
        kelvin,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ConvertTemperatureSoapChannel : WeatherWSLab2.ServiceTemperature.ConvertTemperatureSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConvertTemperatureSoapClient : System.ServiceModel.ClientBase<WeatherWSLab2.ServiceTemperature.ConvertTemperatureSoap>, WeatherWSLab2.ServiceTemperature.ConvertTemperatureSoap {
        
        public ConvertTemperatureSoapClient() {
        }
        
        public ConvertTemperatureSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ConvertTemperatureSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConvertTemperatureSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConvertTemperatureSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double ConvertTemp(double Temperature, WeatherWSLab2.ServiceTemperature.TemperatureUnit FromUnit, WeatherWSLab2.ServiceTemperature.TemperatureUnit ToUnit) {
            return base.Channel.ConvertTemp(Temperature, FromUnit, ToUnit);
        }
        
        public System.Threading.Tasks.Task<double> ConvertTempAsync(double Temperature, WeatherWSLab2.ServiceTemperature.TemperatureUnit FromUnit, WeatherWSLab2.ServiceTemperature.TemperatureUnit ToUnit) {
            return base.Channel.ConvertTempAsync(Temperature, FromUnit, ToUnit);
        }
    }
}
