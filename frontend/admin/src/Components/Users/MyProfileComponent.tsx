import React, {useEffect, useState} from "react";
import axios from "../../Api/axios";

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
        if (state.loaded)
            return;

        const response = await axios.get( process.env.REACT_APP_BACKEND_URL + "/api/users/myprofile");

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
                        <tbody>
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
                        </tbody>
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
