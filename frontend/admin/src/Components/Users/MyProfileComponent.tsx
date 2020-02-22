import React, {useEffect, useState} from "react";
import axios from "../../Api/axios";
import {Simulate} from "react-dom/test-utils";

interface IProfileData {
    login: string;
    email: string;
    displayName: string;
    avatarUrl: string;
}

interface IState {
    loaded: boolean;
    data: IProfileData;
}

const MyProfileComponent = () => {
    const [state, setState] = useState({
        loaded: false
    } as IState);

    useEffect(() => {
        loadProfileData();
    });

    const loadProfileData = async () => {
        const response = await axios.get("http://localhost:5000/api/users/myprofile");
        console.log(response);

        setState({
            loaded: true,
            data: response.data
        });
    };

    const renderProfile = (profileData: IProfileData) => {
        return (
            <div className="container row">
                <div className="col-6">
                    <img  src={profileData.avatarUrl} alt="avatar"/>
                </div>
                <div className="col-6">
                    <table className="table table-info">
                        <tr>
                            <td>
                                <span className="font-weight-bold">Login </span>:
                            </td>
                            <td>
                                <span>{profileData.login}</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span className="font-weight-bold">Email </span>:
                            </td>
                            <td>
                                <span>{profileData.email}</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span className="font-weight-bold">Display name </span>:
                            </td>
                            <td>
                                <span>{profileData.displayName}</span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        )
    };

    let content = state.loaded ? renderProfile(state.data) : <p>Loading...</p>
    return (
        <div>
            {content}
        </div>
    )
};

export default  MyProfileComponent;