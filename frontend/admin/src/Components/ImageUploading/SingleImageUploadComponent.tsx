import React, {useState} from "react";
import {Input} from "reactstrap";

interface IState {
    imageSelected: boolean;
    imageSource: string | null;
}

const SingleImageUploadComponent = () => {
    const [state, setState] = useState({
        imageSelected: false,
        imageSource: null
    } as IState);

    const handleChange = (event: React.SyntheticEvent<HTMLInputElement>) => {
        const files = event.currentTarget.files;
        if (!files)
            return;

        let imageSource = files.length > 0 ? URL.createObjectURL(files[0]) : null;

        setState({
            imageSelected: files.length > 0,
            imageSource: imageSource
        });
    };

    let content = state.imageSelected ? <img src={state.imageSource!} alt="uploadedImage" /> : <p>Upload image</p>
    return (
        <div>
            {content}
            <Input type="file" onChange={handleChange} />
        </div>
    )
};

export default SingleImageUploadComponent;
