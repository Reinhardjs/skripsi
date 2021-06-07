package com.example.a3dtourarestate.main

import com.example.a3dtourarestate.base.BasePresenter
import com.example.a3dtourarestate.base.BaseView
import com.example.a3dtourarestate.model.Estate

interface MainContract {

    interface Presenter : BasePresenter {
        fun fetchData()
    }

    interface View : BaseView<Presenter> {
        fun showLoading()
        fun hideLoading()
        fun onGetSuccessResult(data: List<Estate>?)
        fun onGetFailureResult(message: String)
        fun startUnityActivity(modelUrl: String)
    }

}