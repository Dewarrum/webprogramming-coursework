import React, {useEffect, useState} from "react";
import axios from "../../Api/axios";
import {UserProfileData} from "../../Models/Users/UserProfileData";
import {renderUserProfile} from "../../Models/Users/RenderUtils";
import {useHistory} from "react-router-dom";
import AuthManager from "../../Pages/Authorization/AuthManager";

interface IState {
    loaded: boolean;
    data: UserProfileData;
}

const MyProfileComponent = () => {
    const [state, setState] = useState({
        loaded: false
    } as IState);

    const history = useHistory();

    useEffect(() => {
        loadProfileData();
    });

    const navigateToEdit = () => {
        history.push(`/users/edit/${AuthManager.userSession.id}`);
    };

    const loadProfileData = async () => {
        if (state.loaded)
            return;

        const response = await axios.get( process.env.REACT_APP_BACKEND_URL + "/api/users/myprofile");

        setState({
            loaded: true,
            data: response.data
        });
    };

    let content = state.loaded ? renderUserProfile(state.data) : <p>Loading...</p>
    return (
        <div>
            {content}
            <button onClick={navigateToEdit}>Edit</button>
        </div>
    )
};

export default  MyProfileComponent;
