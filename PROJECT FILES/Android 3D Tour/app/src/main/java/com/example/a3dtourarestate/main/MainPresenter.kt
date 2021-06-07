package com.example.a3dtourarestate.main

import android.util.Log
import com.example.a3dtourarestate.api.EstateApiService
import com.example.a3dtourarestate.model.ApiResponse
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class MainPresenter(private val view: MainContract.View, private val apiService: EstateApiService) :
    MainContract.Presenter {

    override fun fetchData() {

        view.showLoading()
        val call: Call<ApiResponse> = apiService.getEstates()

        call.enqueue(object : Callback<ApiResponse> {

            override fun onResponse(call: Call<ApiResponse>?, response: Response<ApiResponse>?) {
                try {
                    Log.d("SKRIPSHIT", response?.toString())

                    if (response!!.isSuccessful) {

                        val data: ApiResponse? = response.body()
                        view.onGetSuccessResult(data?.result)
                        view.hideLoading()

                    } else {
                        view.hideLoading()
                        view.onGetFailureResult(response.message())
                    }

                } catch (e: Exception) {
                    view.hideLoading()
                    view.onGetFailureResult(e.message!!)
                }
            }

            override fun onFailure(call: Call<ApiResponse>?, t: Throwable?) {
                view.hideLoading()
                view.onGetFailureResult(t!!.message!!)
            }
        })
    }

    override fun onViewCreated() {
        TODO("Not yet implemented")
    }

    override fun onDestroy() {
        TODO("Not yet implemented")
    }
}