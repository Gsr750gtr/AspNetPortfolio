package com.example.aspnetandroidsample.data.CustomerApi

import com.example.aspnetandroidsample.data.dto.CustomerDto
import retrofit2.http.GET
import retrofit2.http.Path

interface CustomerApi {
    @GET("api/customers/{code}") // ASP.NETのルーティングに合わせて変更してください
    suspend fun getCustomer(
        @Path("code") code: String
    ): CustomerDto
}