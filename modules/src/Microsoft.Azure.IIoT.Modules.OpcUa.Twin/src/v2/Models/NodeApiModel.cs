// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Node model for module
    /// </summary>
    [DataContract]
    public class NodeApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public NodeApiModel() { }

        /// <summary>
        /// Create node api model from service model
        /// </summary>
        /// <param name="model"></param>
        public NodeApiModel(NodeModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            NodeId = model.NodeId;
            Children = model.Children;
            BrowseName = model.BrowseName;
            DisplayName = model.DisplayName;
            Description = model.Description;
            NodeClass = model.NodeClass;
            IsAbstract = model.IsAbstract;
            AccessLevel = model.AccessLevel;
            EventNotifier = model.EventNotifier;
            Executable = model.Executable;
            DataType = model.DataType;
            ValueRank = model.ValueRank;
            AccessRestrictions = model.AccessRestrictions;
            ArrayDimensions = model.ArrayDimensions;
            ContainsNoLoops = model.ContainsNoLoops;
            DataTypeDefinition = model.DataTypeDefinition;
            Value = model.Value;
            Historizing = model.Historizing;
            InverseName = model.InverseName;
            MinimumSamplingInterval = model.MinimumSamplingInterval;
            Symmetric = model.Symmetric;
            UserAccessLevel = model.UserAccessLevel;
            UserExecutable = model.UserExecutable;
            UserWriteMask = model.UserWriteMask;
            WriteMask = model.WriteMask;
            RolePermissions = model.RolePermissions?
                .Select(p => p == null ? null : new RolePermissionApiModel(p))
                .ToList();
            UserRolePermissions = model.UserRolePermissions?
                .Select(p => p == null ? null : new RolePermissionApiModel(p))
                .ToList();
            TypeDefinitionId = model.TypeDefinitionId;
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public NodeModel ToServiceModel() {
            return new NodeModel {
                NodeId = NodeId,
                Children = Children,
                BrowseName = BrowseName,
                DisplayName = DisplayName,
                Description = Description,
                NodeClass = NodeClass,
                IsAbstract = IsAbstract,
                AccessLevel = AccessLevel,
                EventNotifier = EventNotifier,
                Executable = Executable,
                DataType = DataType,
                ValueRank = ValueRank,
                AccessRestrictions = AccessRestrictions,
                ArrayDimensions = ArrayDimensions,
                ContainsNoLoops = ContainsNoLoops,
                DataTypeDefinition = DataTypeDefinition,
                Value = Value,
                Historizing = Historizing,
                InverseName = InverseName,
                MinimumSamplingInterval = MinimumSamplingInterval,
                Symmetric = Symmetric,
                UserAccessLevel = UserAccessLevel,
                UserExecutable = UserExecutable,
                UserWriteMask = UserWriteMask,
                WriteMask = WriteMask,
                RolePermissions = RolePermissions?
                    .Select(p => p.ToServiceModel())
                    .ToList(),
                UserRolePermissions = UserRolePermissions?
                    .Select(p => p.ToServiceModel())
                    .ToList(),
                TypeDefinitionId = TypeDefinitionId
            };
        }

        /// <summary>
        /// Type of node
        /// </summary>
        [DataMember(Name = "NodeClass",
            EmitDefaultValue = false)]
        public NodeClass? NodeClass { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        [DataMember(Name = "DisplayName",
            EmitDefaultValue = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Id of node.
        /// (Mandatory).
        /// </summary>
        [DataMember(Name = "NodeId")]
        public string NodeId { get; set; }

        /// <summary>
        /// Description if any
        /// </summary>
        [DataMember(Name = "Description",
            EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Browse name
        /// </summary>
        [DataMember(Name = "BrowseName",
            EmitDefaultValue = false)]
        public string BrowseName { get; set; }

        /// <summary>
        /// Node access restrictions if any.
        /// (default: none)
        /// </summary>
        [DataMember(Name = "AccessRestrictions",
            EmitDefaultValue = false)]
        public NodeAccessRestrictions? AccessRestrictions { get; set; }

        /// <summary>
        /// Default write mask for the node
        /// (default: 0)
        /// </summary>
        [DataMember(Name = "writeMask",
            EmitDefaultValue = false)]
        public uint? WriteMask { get; set; }

        /// <summary>
        /// User write mask for the node
        /// (default: 0)
        /// </summary>
        [DataMember(Name = "UserWriteMask",
            EmitDefaultValue = false)]
        public uint? UserWriteMask { get; set; }

        /// <summary>
        /// Whether type is abstract, if type can
        /// be abstract.  Null if not type node.
        /// (default: false)
        /// </summary>
        [DataMember(Name = "IsAbstract",
            EmitDefaultValue = false)]
        public bool? IsAbstract { get; set; }

        /// <summary>
        /// Whether a view contains loops. Null if
        /// not a view.
        /// </summary>
        [DataMember(Name = "ContainsNoLoops",
            EmitDefaultValue = false)]
        public bool? ContainsNoLoops { get; set; }

        /// <summary>
        /// If object or view and eventing, event notifier
        /// to subscribe to.
        /// (default: no events supported)
        /// </summary>
        [DataMember(Name = "EventNotifier",
            EmitDefaultValue = false)]
        public NodeEventNotifier? EventNotifier { get; set; }

        /// <summary>
        /// If method node class, whether method can
        /// be called.
        /// </summary>
        [DataMember(Name = "Executable",
            EmitDefaultValue = false)]
        public bool? Executable { get; set; }

        /// <summary>
        /// If method node class, whether method can
        /// be called by current user.
        /// (default: false if not executable)
        /// </summary>
        [DataMember(Name = "UserExecutable",
            EmitDefaultValue = false)]
        public bool? UserExecutable { get; set; }

        /// <summary>
        /// Data type definition in case node is a
        /// data type node and definition is available,
        /// otherwise null.
        /// </summary>
        [DataMember(Name = "DataTypeDefinition",
            EmitDefaultValue = false)]
        public JToken DataTypeDefinition { get; set; }

        /// <summary>
        /// Default access level for variable node.
        /// (default: 0)
        /// </summary>
        [DataMember(Name = "AccessLevel",
            EmitDefaultValue = false)]
        public NodeAccessLevel? AccessLevel { get; set; }

        /// <summary>
        /// User access level for variable node or null.
        /// (default: 0)
        /// </summary>
        [DataMember(Name = "UserAccessLevel",
            EmitDefaultValue = false)]
        public NodeAccessLevel? UserAccessLevel { get; set; }

        /// <summary>
        /// If variable the datatype of the variable.
        /// (default: null)
        /// </summary>
        [DataMember(Name = "DataType",
            EmitDefaultValue = false)]
        public string DataType { get; set; }

        /// <summary>
        /// Value rank of the variable data of a variable
        /// or variable type, otherwise null.
        /// (default: scalar = -1)
        /// </summary>
        [DataMember(Name = "ValueRank",
            EmitDefaultValue = false)]
        public NodeValueRank? ValueRank { get; set; }

        /// <summary>
        /// Array dimensions of variable or variable type.
        /// (default: empty array)
        /// </summary>
        [DataMember(Name = "ArrayDimensions",
            EmitDefaultValue = false)]
        public uint[] ArrayDimensions { get; set; }

        /// <summary>
        /// Whether the value of a variable is historizing.
        /// (default: false)
        /// </summary>
        [DataMember(Name = "Historizing",
            EmitDefaultValue = false)]
        public bool? Historizing { get; set; }

        /// <summary>
        /// Minimum sampling interval for the variable
        /// value, otherwise null if not a variable node.
        /// (default: null)
        /// </summary>
        [DataMember(Name = "MinimumSamplingInterval",
            EmitDefaultValue = false)]
        public double? MinimumSamplingInterval { get; set; }

        /// <summary>
        /// Value of variable or default value of the
        /// subtyped variable in case node is a variable
        /// type, otherwise null.
        /// </summary>
        [DataMember(Name = "Value",
            EmitDefaultValue = false)]
        public JToken Value { get; set; }

        /// <summary>
        /// Inverse name of the reference if the node is
        /// a reference type, otherwise null.
        /// </summary>
        [DataMember(Name = "InverseName",
            EmitDefaultValue = false)]
        public string InverseName { get; set; }

        /// <summary>
        /// Whether the reference is symmetric in case
        /// the node is a reference type, otherwise
        /// null.
        /// </summary>
        [DataMember(Name = "Symmetric",
            EmitDefaultValue = false)]
        public bool? Symmetric { get; set; }

        /// <summary>
        /// Role permissions
        /// </summary>
        [DataMember(Name = "RolePermissions",
            EmitDefaultValue = false)]
        public List<RolePermissionApiModel> RolePermissions { get; set; }

        /// <summary>
        /// User Role permissions
        /// </summary>
        [DataMember(Name = "UserRolePermissions",
            EmitDefaultValue = false)]
        public List<RolePermissionApiModel> UserRolePermissions { get; set; }

        /// <summary>
        /// Optional type definition of the node
        /// </summary>
        [DataMember(Name = "TypeDefinitionId",
            EmitDefaultValue = false)]
        public string TypeDefinitionId { get; set; }

        /// <summary>
        /// Whether node has children which are defined as
        /// any forward hierarchical references.
        /// (default: unknown)
        /// </summary>
        [DataMember(Name = "Children",
            EmitDefaultValue = false)]
        public bool? Children { get; set; }
    }
}
