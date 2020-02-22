import React from 'react';
import { Route } from 'react-router';
import './App.css';
import {Layout} from "./Pages/Layout/Layout";
import {Home} from "./Pages/Home/Home";
import {UserListComponent} from "./Components/Users/UserListComponent";
import {BrowserRouter, Switch} from "react-router-dom";
import {useRoutes} from 'hookrouter';
import {UserProfileComponent} from "./Components/Users/UserProfileComponent";
import {routes} from "./Routes/Routes";
import LoginPage from "./Pages/Login/LoginPage";
import Authorize from "./Pages/Authorization/Authorize";
import SignIn from "./Pages/Authorization/SignIn";

export const App: React.FC = () => {
    return (
        <BrowserRouter>
            <Switch>
                <Route exact path={routes.login} component={LoginPage} />
                <Route exact path={routes.signIn} component={SignIn} />
                <Layout>
                    <Authorize>
                        <Route excat path={routes.home} component={Home} />
                    </Authorize>
                </Layout>
            </Switch>
        </BrowserRouter>
    )
};
