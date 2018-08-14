using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data {
    public enum MessageType { OK, Invalid, Unauthorized }
    public class Message {
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType Type { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityState Action { get; set; } = EntityState.Unchanged;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Message> InnerMessages { get; set; }

        public static Message OK() {
            return new Message {
                Type = MessageType.OK
            };
        }
        public static Message OK(string name) {
            return new Message {
                Type = MessageType.OK,
                Name = name
            };
        }
        public static Message OK(string name, List<Message> innerMessages) {
            return new Message {
                Type = MessageType.OK,
                Name = name,
                InnerMessages = innerMessages
            };
        }
 
        public static Message Invalid(string name) {
            return new Message {
                Type = MessageType.Invalid,
                Name = name
            };
        }
        public static Message Invalid(string name, string description) {
            return new Message {
                Type = MessageType.Invalid,
                Name = name,
                Description = description
            };
        }
        public static Message Invalid(string name, List<Message> innerMessage) {
            return new Message {
                Type = MessageType.Invalid,
                Name = name,
                InnerMessages = innerMessage
            };
        }
        public static Message Invalid(string name, string description, List<Message> innerMessages) {
            return new Message {
                Type = MessageType.Invalid,
                Name = name,
                Description = description,
                InnerMessages = innerMessages
            };
        }

        public static Message Unauthorized(string name) {
            return new Message {
                Type = MessageType.Unauthorized,
                Name = name
            };
        }
        public static Message Unauthorized(string name, string description) {
            return new Message {
                Type = MessageType.Unauthorized,
                Name = name,
                Description = description
            };
        }
        public static Message Unauthorized(string name, List<Message> innerMessage) {
            return new Message {
                Type = MessageType.Unauthorized,
                Name = name,
                InnerMessages = innerMessage
            };
        }
        public static Message Unauthorized(string name, string description, List<Message> innerMessages) {
            return new Message {
                Type = MessageType.Unauthorized,
                Name = name,
                Description = description,
                InnerMessages = innerMessages
            };
        }
    }
}
