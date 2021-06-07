package com.example.a3dtourarestate.model

import com.google.gson.annotations.SerializedName

data class Estate(
    @SerializedName("id") val id: String,
    @SerializedName("title") val title: String,
    @SerializedName("description") val description: String,
    @SerializedName("thumbnailUrl") val thumbnailUrl: String,
    @SerializedName("modelUrl") val modelUrl: String
)