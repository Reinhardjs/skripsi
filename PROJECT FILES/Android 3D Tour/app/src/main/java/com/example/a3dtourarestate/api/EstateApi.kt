package com.example.a3dtourarestate.api

import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class EstateApi {
    companion object {
        fun getClient(): Retrofit {
            val client = OkHttpClient.Builder()
            client.addInterceptor(AuthInterceptor())

            // To display the request and response details in log
            val interceptor = HttpLoggingInterceptor()
            interceptor.level = HttpLoggingInterceptor.Level.HEADERS
            client.addInterceptor(interceptor)

            return Retrofit.Builder()
                .baseUrl("https://api-skripsi.herokuapp.com/")
                //.addCallAdapterFactory(RxJava2CallAdapterFactory.create())
                .addConverterFactory(GsonConverterFactory.create())
                .client(client.build())
                .build()
        }
    }

}