/**
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 *
 * Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

package com.microsoft.azure.iiot.opc.vault.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Certificate request record model.
 */
public class CertificateRequestRecordApiModel {
    /**
     * Request id.
     */
    @JsonProperty(value = "requestId")
    private String requestId;

    /**
     * Application id.
     */
    @JsonProperty(value = "entityId")
    private String entityId;

    /**
     * Trust group.
     */
    @JsonProperty(value = "groupId")
    private String groupId;

    /**
     * Possible values include: 'New', 'Approved', 'Rejected', 'Failure',
     * 'Completed', 'Accepted'.
     */
    @JsonProperty(value = "state")
    private CertificateRequestState state;

    /**
     * Possible values include: 'SigningRequest', 'KeyPairRequest'.
     */
    @JsonProperty(value = "type")
    private CertificateRequestType type;

    /**
     * Error diagnostics.
     */
    @JsonProperty(value = "errorInfo")
    private Object errorInfo;

    /**
     * The submitted property.
     */
    @JsonProperty(value = "submitted")
    private VaultOperationContextApiModel submitted;

    /**
     * The approved property.
     */
    @JsonProperty(value = "approved")
    private VaultOperationContextApiModel approved;

    /**
     * The accepted property.
     */
    @JsonProperty(value = "accepted")
    private VaultOperationContextApiModel accepted;

    /**
     * Get request id.
     *
     * @return the requestId value
     */
    public String requestId() {
        return this.requestId;
    }

    /**
     * Set request id.
     *
     * @param requestId the requestId value to set
     * @return the CertificateRequestRecordApiModel object itself.
     */
    public CertificateRequestRecordApiModel withRequestId(String requestId) {
        this.requestId = requestId;
        return this;
    }

    /**
     * Get application id.
     *
     * @return the entityId value
     */
    public String entityId() {
        return this.entityId;
    }

    /**
     * Set application id.
     *
     * @param entityId the entityId value to set
     * @return the CertificateRequestRecordApiModel object itself.
     */
    public CertificateRequestRecordApiModel withEntityId(String entityId) {
        this.entityId = entityId;
        return this;
    }

    /**
     * Get trust group.
     *
     * @return the groupId value
     */
    public String groupId() {
        return this.groupId;
    }

    /**
     * Set trust group.
     *
     * @param groupId the groupId value to set
     * @return the CertificateRequestRecordApiModel object itself.
     */
    public CertificateRequestRecordApiModel withGroupId(String groupId) {
        this.groupId = groupId;
        return this;
    }

    /**
     * Get possible values include: 'New', 'Approved', 'Rejected', 'Failure', 'Completed', 'Accepted'.
     *
     * @return the state value
     */
    public CertificateRequestState state() {
        return this.state;
    }

    /**
     * Set possible values include: 'New', 'Approved', 'Rejected', 'Failure', 'Completed', 'Accepted'.
     *
     * @param state the state value to set
     * @return the CertificateRequestRecordApiModel object itself.
     */
    public CertificateRequestRecordApiModel withState(CertificateRequestState state) {
        this.state = state;
        return this;
    }

    /**
     * Get possible values include: 'SigningRequest', 'KeyPairRequest'.
     *
     * @return the type value
     */
    public CertificateRequestType type() {
        return this.type;
    }

    /**
     * Set possible values include: 'SigningRequest', 'KeyPairRequest'.
     *
     * @param type the type value to set
     * @return the CertificateRequestRecordApiModel object itself.
     */
    public CertificateRequestRecordApiModel withType(CertificateRequestType type) {
        this.type = type;
        return this;
    }

    /**
     * Get error diagnostics.
     *
     * @return the errorInfo value
     */
    public Object errorInfo() {
        return this.errorInfo;
    }

    /**
     * Set error diagnostics.
     *
     * @param errorInfo the errorInfo value to set
     * @return the CertificateRequestRecordApiModel object itself.
     */
    public CertificateRequestRecordApiModel withErrorInfo(Object errorInfo) {
        this.errorInfo = errorInfo;
        return this;
    }

    /**
     * Get the submitted value.
     *
     * @return the submitted value
     */
    public VaultOperationContextApiModel submitted() {
        return this.submitted;
    }

    /**
     * Set the submitted value.
     *
     * @param submitted the submitted value to set
     * @return the CertificateRequestRecordApiModel object itself.
     */
    public CertificateRequestRecordApiModel withSubmitted(VaultOperationContextApiModel submitted) {
        this.submitted = submitted;
        return this;
    }

    /**
     * Get the approved value.
     *
     * @return the approved value
     */
    public VaultOperationContextApiModel approved() {
        return this.approved;
    }

    /**
     * Set the approved value.
     *
     * @param approved the approved value to set
     * @return the CertificateRequestRecordApiModel object itself.
     */
    public CertificateRequestRecordApiModel withApproved(VaultOperationContextApiModel approved) {
        this.approved = approved;
        return this;
    }

    /**
     * Get the accepted value.
     *
     * @return the accepted value
     */
    public VaultOperationContextApiModel accepted() {
        return this.accepted;
    }

    /**
     * Set the accepted value.
     *
     * @param accepted the accepted value to set
     * @return the CertificateRequestRecordApiModel object itself.
     */
    public CertificateRequestRecordApiModel withAccepted(VaultOperationContextApiModel accepted) {
        this.accepted = accepted;
        return this;
    }

}