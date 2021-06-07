package com.example.a3dtourarestate.di

import com.example.a3dtourarestate.api.EstateApiService

interface DependencyInjector {

    fun getApiService(): EstateApiService

}