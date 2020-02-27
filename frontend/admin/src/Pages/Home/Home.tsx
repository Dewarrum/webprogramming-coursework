import React from 'react';
import MultipleImagesUploadComponent from "../../Components/ImageUploading/MultipleImagesUploadComponent";
import ImageViewerComponent from "../../Components/ImageViewing/ImageViewerComponent";

export const Home : React.FC = () => {
    return (
        <div>
            <h1>Web Programming Coursework</h1>
            <p>Service to help people express their feelings through art</p>
            <MultipleImagesUploadComponent/>
            <ImageViewerComponent imageId={1}/>
        </div>
    )
};
