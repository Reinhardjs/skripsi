package com.example.a3dtourarestate.main

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.afollestad.materialdialogs.MaterialDialog
import com.example.a3dtourarestate.EstateApplication
import com.example.a3dtourarestate.R
import com.example.a3dtourarestate.adapter.EstateAdapter
import com.example.a3dtourarestate.model.Estate
import com.unity3d.player.UnityPlayerActivity
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity(), MainContract.View {

    private lateinit var materialDialog: MaterialDialog
    private lateinit var mainPresenter: MainContract.Presenter
    private lateinit var estateAdapter: EstateAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        setPresenter(
            MainPresenter(
                this,
                EstateApplication.getDependencyInjection(this).getApiService()
            )
        )

        val layoutManager = LinearLayoutManager(this)
        layoutManager.orientation = LinearLayoutManager.VERTICAL
        recyclerView.layoutManager = layoutManager

        estateAdapter = EstateAdapter(baseContext)
        estateAdapter.setOnItemClickListener(object : EstateAdapter.OnItemClickListener {
            override fun onItemClicked(data: Estate) {
                startUnityActivity(data.modelUrl)
            }
        })
        recyclerView.adapter = estateAdapter

        mainPresenter.fetchData()
    }

    override fun showLoading() {
        materialDialog = MaterialDialog.Builder(this)
            .title("Perumahan Balimbingan Permai")
            .content("loading...")
            .progress(true, 0)
            .show()

    }

    override fun hideLoading() {
        materialDialog.dismiss()
    }

    override fun onGetSuccessResult(data: List<Estate>?) {
        estateAdapter.data = data
        estateAdapter.notifyDataSetChanged()

    }

    override fun onGetFailureResult(message: String) {
        Toast.makeText(this, message, Toast.LENGTH_SHORT).show()
    }

    override fun startUnityActivity(modelUrl: String) {
        // "https://gitlab.com/reinhardjonathansilalahi/3dmodel/-/raw/master/3dmodel"

        val intent = Intent(this, UnityPlayerActivity::class.java)
        intent.putExtra("modelUrl", modelUrl)
        startActivity(intent)
    }

    override fun setPresenter(presenter: MainContract.Presenter) {
        mainPresenter = presenter
    }

}