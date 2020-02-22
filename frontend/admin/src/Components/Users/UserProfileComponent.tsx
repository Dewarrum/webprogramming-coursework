import React, {useEffect, useState} from 'react';
import { useParams } from 'react-router-dom';
import axios from "../../Api/axios";

interface IUserProfileData {
    login: string;
    displayName: string;
    email: string;
    id: number;
    avatarUrl: string;
}

interface IState {
    loaded: boolean;
    data: IUserProfileData;
}

function renderUserProfile(userProfileData: IUserProfileData) {
    return (
        <div className="container row">
            <div className="col-6">
                <img  src={userProfileData.avatarUrl} alt="avatar"/>
            </div>
            <div className="col-6">
                <table className="table table-info">
                    <tbody>
                    <tr>
                        <td>
                            <span className="font-weight-bold">Login </span>:
                        </td>
                        <td>
                            <span>{userProfileData.login}</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span className="font-weight-bold">Email </span>:
                        </td>
                        <td>
                            <span>{userProfileData.email}</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span className="font-weight-bold">Display name </span>:
                        </td>
                        <td>
                            <span>{userProfileData.displayName}</span>
                        </td>
                    </tr>
                    </tbody>
                </table>
            </div>
        </div>
    )
}

const UserProfileComponent = () => {
    const { id } = useParams();
    const [state, setState] = useState({
        loaded: false
    } as IState);

    useEffect(() => {
        loadUserProfileData();
    });

    const loadUserProfileData = async () => {
        if (state.loaded)
            return;

        const response = await axios.get<IUserProfileData>(`${process.env.REACT_APP_BACKEND_URL}/api/users/profile/${id}`);

        const newState = {
            loaded: true,
            data: response.data
        } as IState;

        setState(newState);
    };

    let content = state.loaded ? renderUserProfile(state.data) : <p>Loading...</p>
    return (
        <div>{content}</div>
    )
};

export default UserProfileComponent;
