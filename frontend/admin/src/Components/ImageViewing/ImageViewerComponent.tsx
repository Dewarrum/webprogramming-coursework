import React, {useEffect, useState} from "react";
import axios from "../../Api/axios";

interface IImageViewerComponentProps {
    imageId: number;
}

const ImageViewerComponent = (props: IImageViewerComponentProps) => {
    const [state, setState] = useState({
        data: ""
    });
    const loadImage = (id: number) => {
        axios.get(`${process.env.REACT_APP_BACKEND_URL}/api/images/1`)
            .then(res => {
                if (state.data)
                    return;

                setState({
                    data: res.data
                });
                console.log(res);
            })
            .catch(err => console.log(err));
    }

    useEffect(() => {
        loadImage(props.imageId);
    });

    let content = state.data ? <img src={state.data} alt="image" /> : <p>Loading...</p>

    return (
        <div>{content}</div>
    )
};

export default ImageViewerComponent;
