import React, {useState} from "react";
import {Button, Input} from "reactstrap";
import {uploadImage} from "./ImageUploadHelper";
import {toast} from "react-toastify";

interface IState {
    imageSelected: boolean;
    imageSource: string | null;
    image: File
}

interface IImageUploadState {
    loading: boolean;
    loaded: boolean
};

const SingleImageUploadComponent = () => {
    const [uploadState, setUploadState] = useState({
        loaded: false,
        loading: false
    } as IImageUploadState);
    
    const [state, setState] = useState({
        imageSelected: false,
        imageSource: null
    } as IState);

    const handleChange = (event: React.SyntheticEvent<HTMLInputElement>) => {
        const files = event.currentTarget.files || new FileList();
        if (!files)
            return;

        let imageSource = files.length > 0 ? URL.createObjectURL(files[0]) : null;

        setState({
            imageSelected: files.length > 0,
            imageSource: imageSource,
            image: files[0]
        });
    };

    const onUploadClicked = () => {
        const formData = new FormData();
        formData.append("images", state.image);

        const config = {
            onUploadProgress: function (progressEvent: any) {
                const percent = Math.round(progressEvent.loaded / progressEvent.total * 100);
            }
        }
    };

    let content = state.imageSelected ? <img src={state.imageSource!} alt="uploadedImage" className="h-100" /> : <p>Upload image</p>
    return (
        <div className="h-100">
            {content}
            <Input type="file" onChange={handleChange} />
            <Button onClick={onUploadClicked}>Upload</Button>
        </div>
    )
};

export default SingleImageUploadComponent;
