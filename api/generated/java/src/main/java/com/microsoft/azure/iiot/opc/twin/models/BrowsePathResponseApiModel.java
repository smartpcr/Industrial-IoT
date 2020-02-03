/**
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 *
 * Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

package com.microsoft.azure.iiot.opc.twin.models;

import java.util.List;
import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Result of node browse continuation.
 */
public class BrowsePathResponseApiModel {
    /**
     * Targets.
     */
    @JsonProperty(value = "targets")
    private List<NodePathTargetApiModel> targets;

    /**
     * The errorInfo property.
     */
    @JsonProperty(value = "errorInfo")
    private ServiceResultApiModel errorInfo;

    /**
     * Get targets.
     *
     * @return the targets value
     */
    public List<NodePathTargetApiModel> targets() {
        return this.targets;
    }

    /**
     * Set targets.
     *
     * @param targets the targets value to set
     * @return the BrowsePathResponseApiModel object itself.
     */
    public BrowsePathResponseApiModel withTargets(List<NodePathTargetApiModel> targets) {
        this.targets = targets;
        return this;
    }

    /**
     * Get the errorInfo value.
     *
     * @return the errorInfo value
     */
    public ServiceResultApiModel errorInfo() {
        return this.errorInfo;
    }

    /**
     * Set the errorInfo value.
     *
     * @param errorInfo the errorInfo value to set
     * @return the BrowsePathResponseApiModel object itself.
     */
    public BrowsePathResponseApiModel withErrorInfo(ServiceResultApiModel errorInfo) {
        this.errorInfo = errorInfo;
        return this;
    }

}