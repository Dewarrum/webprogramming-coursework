import {UserProfileData} from "./UserProfileData";
import React from "react";

export function renderUserProfile(userProfileData: UserProfileData) {
    return (
        <div className="container row">
            <div className="col-6">
                <img src={userProfileData.avatarUrl} alt="avatar" style={{maxHeight: "300px"}}/>
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
