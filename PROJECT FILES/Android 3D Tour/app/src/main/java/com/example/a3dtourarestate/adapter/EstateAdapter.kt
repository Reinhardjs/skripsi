package com.example.a3dtourarestate.adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.bumptech.glide.request.RequestOptions
import com.example.a3dtourarestate.R
import com.example.a3dtourarestate.model.Estate
import kotlinx.android.synthetic.main.item_layout.view.*


class EstateAdapter(var context: Context) : RecyclerView.Adapter<EstateAdapter.ViewHolder>() {

    var data: List<Estate>? = ArrayList()
    var itemClickListener: OnItemClickListener? = null

    override fun onCreateViewHolder(parent: ViewGroup, p1: Int): ViewHolder {
        val view = LayoutInflater.from(context).inflate(R.layout.item_layout, parent, false);
        return ViewHolder(view)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.itemView.title.text = data?.get(position)?.title ?: ""
        val options: RequestOptions = RequestOptions()
            .centerCrop()
            .placeholder(R.mipmap.ic_launcher_round)
            .error(R.mipmap.ic_launcher_round)

        Glide.with(context).load(data?.get(position)?.thumbnailUrl).apply(options)
            .into(holder.itemView.image)
    }

    override fun getItemCount(): Int {
        return data?.size!!
    }

    fun setOnItemClickListener(itemClickListener: OnItemClickListener) {
        this.itemClickListener = itemClickListener
    }

    inner class ViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {

        init {
            itemView.setOnClickListener {
                data?.get(adapterPosition)?.let { it2 -> itemClickListener?.onItemClicked(it2) }
            }
        }

    }

    interface OnItemClickListener {
        fun onItemClicked(data: Estate)
    }

}