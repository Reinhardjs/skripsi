package com.example.a3dtourarestate.di

import com.example.a3dtourarestate.api.EstateApi
import com.example.a3dtourarestate.api.EstateApiService

class DependencyInjectorImpl : DependencyInjector {
    private val estateApiService: EstateApiService =
        EstateApi.getClient().create(EstateApiService::class.java)

    override fun getApiService(): EstateApiService {
        return this.estateApiService
    }
}