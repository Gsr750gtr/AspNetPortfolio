package com.example.aspnetandroidsample.data.dto

import com.example.aspnetandroidsample.model.CustomerInfo

data class CustomerDto(
    val code: String?,
    val name: String?,
    val nameKana: String?,
    val prefecture: String?,
) {
    fun toCustomerInfo() = CustomerInfo(code ?: "", name ?: "", nameKana ?: "", prefecture ?: "")
}
