package com.example.aspnetandroidsample.data.repository

import com.example.aspnetandroidsample.data.CustomerApi.ApiClient
import com.example.aspnetandroidsample.model.CustomerInfo
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import retrofit2.HttpException
import java.net.SocketTimeoutException

class CustomerRepository {
    private val api = ApiClient.customerApi
    suspend fun getCustomer(code: String): CustomerInfo? = withContext(Dispatchers.IO) {
        return@withContext try {
            api.getCustomer(code).toCustomerInfo()
        } catch (e: Exception) {
            e.printStackTrace()
            // ViewModelには該当なしor通信エラーだけ返し、トーストを表示させる
            if (e is HttpException && e.code() == 404) {
                null
            } else {
                throw e
            }
        }
    }
}