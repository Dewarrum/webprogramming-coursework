import React from 'react';
import { Route } from 'react-router';
import './App.css';
import {Layout} from "./Pages/Layout/Layout";
import {Home} from "./Pages/Home/Home";
import {UserListComponent} from "./Components/Users/UserListComponent";
import {BrowserRouter, Switch} from "react-router-dom";
import {useRoutes} from 'hookrouter';
import {Routes} from "./Routes/Routes";
import {UserProfileComponent} from "./Components/Users/UserProfileComponent";

export const App: React.FC = () => {
    const routeResult = useRoutes(Routes);
    return (
        <Layout>
            {routeResult}
        </Layout>
    )
};

/* return (
    <Layout>
        <BrowserRouter>
            <Switch>
                <Route exact path="/">
                    <Home />
                </Route>
                <Route path="/users/list">
                    <UserListComponent />
                </Route>
            </Switch>
        </BrowserRouter>
    </Layout>
); */
