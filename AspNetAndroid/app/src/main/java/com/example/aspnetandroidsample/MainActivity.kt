package com.example.aspnetandroidsample

import android.os.Bundle
import android.widget.Toast
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.text.KeyboardActions
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.aspnetandroidsample.model.CustomerInfo
import com.example.aspnetandroidsample.ui.theme.AspNetAndroidSampleTheme
import com.example.aspnetandroidsample.viewmodel.CustomerInfoViewModel
import androidx.compose.runtime.getValue
import androidx.compose.ui.platform.LocalContext
import androidx.lifecycle.viewmodel.compose.viewModel

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
            AspNetAndroidSampleTheme {
                Scaffold(modifier = Modifier.fillMaxSize()) { innerPadding ->
                    InputCustomer(
                        modifier = Modifier.padding(innerPadding)
                    )
                }
            }
        }
    }
}

@Composable
fun InputCustomer(
    modifier: Modifier = Modifier,
    viewModel: CustomerInfoViewModel = viewModel()
) {
    val context = LocalContext.current // トースト表示に必要
    val errorMessage = viewModel.errorMessage // ViewModelのエラー状態を取得

    // errorMessageが更新されるたびに実行される
    LaunchedEffect(errorMessage) {
        errorMessage?.let {
            Toast.makeText(context, it, Toast.LENGTH_SHORT).show()
            viewModel.clearErrorMessage() // 表示したらクリアする
        }
    }

    Column(modifier = modifier) {
        val customerInfo by viewModel.customerInfo
        var inputText by remember { mutableStateOf("") }

        TextField(
            modifier = Modifier
                .fillMaxWidth()
                .padding(0.dp, 20.dp),
            value = inputText,
            onValueChange = { newValue ->
                inputText = newValue
            },
            label = { Text("メッセージを入力") },
            singleLine = true,
            keyboardOptions = KeyboardOptions(imeAction = ImeAction.Send),
            keyboardActions = KeyboardActions(onSend = {
                viewModel.fetchCustomer(inputText)
            })
        )
        ShowCustomer(customerInfo ?: CustomerInfo("", "", "", ""))
    }
}

@Composable
fun ShowCustomer(
    customerInfo: CustomerInfo,
    modifier: Modifier = Modifier
) {
    Column(modifier) {
        InfoText("取引先コード", customerInfo.code)
        InfoText("取引先名", customerInfo.name)
        InfoText("取引先名カナ", customerInfo.nameKana)
        InfoText("都道府県名", customerInfo.prefecture)
    }
}

@Composable
fun InfoText(title: String, value: String) {
    Row() {
        Text(text = title, Modifier.width(110.dp))
        Text(text = ":", Modifier.padding(5.dp, 0.dp, 5.dp, 0.dp))
        Text(text = value)
    }
}

@Preview(showBackground = true, widthDp = 520)
@Composable
fun CustomerInfoPreview() {
    AspNetAndroidSampleTheme {
        InputCustomer()
    }
}

@Preview(showBackground = true, widthDp = 320)
@Composable
fun CustomerPreview() {
    AspNetAndroidSampleTheme {
        ShowCustomer(CustomerInfo("C001", "会社０１", "カイシャ", "東京"))
    }
}