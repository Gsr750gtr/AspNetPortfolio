package com.example.aspnetandroidsample.viewmodel

import androidx.compose.runtime.State // 追加が必要
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.aspnetandroidsample.data.repository.CustomerRepository
import com.example.aspnetandroidsample.model.CustomerInfo
import kotlinx.coroutines.launch
import java.net.SocketTimeoutException

class CustomerInfoViewModel : ViewModel() {
    private val _customerData = mutableStateOf<CustomerInfo?>(null)
    val customerInfo: State<CustomerInfo?> = _customerData

    // トースト表示用のイベント状態（表示したら空に戻す）
    var errorMessage by mutableStateOf<String?>(null)
        private set
    private val repository = CustomerRepository()

    fun fetchCustomer(code: String) {
        viewModelScope.launch {
            try {
                val result = repository.getCustomer(code)
                _customerData.value = repository.getCustomer(code)

                errorMessage = if (result == null) {
                    "取引先が見つかりません"
                } else {
                    null
                }
            } catch (e: Exception) {
                errorMessage = "通信エラーが発生しました"
            }
        }
    }

    // メッセージを表示した後にクリアするための関数
    fun clearErrorMessage() {
        errorMessage = null
    }
}