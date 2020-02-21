import {Home} from '../Pages/Home/Home';
import {UserListComponent} from "../Components/Users/UserListComponent";
import React from "react";
import {UserProfileComponent} from "../Components/Users/UserProfileComponent";

export const Routes = {
    "/": () => <Home/>,
    "/list": () => <UserListComponent />,
    "/profile": () => <UserProfileComponent />
};
