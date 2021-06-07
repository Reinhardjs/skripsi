package com.example.a3dtourarestate.api

import com.example.a3dtourarestate.model.ApiResponse
import retrofit2.Call
import retrofit2.http.GET

interface EstateApiService {

    //@GET("api/v1/search")
    //fun getEstates(@Query("query") query: String, @Query("count") count: Int): Observable<ResponseObject>

    @GET("api/estates/get")
    fun getEstates(): Call<ApiResponse>
}