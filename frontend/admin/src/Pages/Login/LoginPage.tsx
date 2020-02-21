import React, {FormEvent, useState} from "react";
import AuthManager from "../Authorization/AuthManager";
import { Redirect } from "react-router-dom";
import {buildUrl, routes} from "../../Routes/Routes";
import {Button, Card, CardBody, Container, Input, Label} from "reactstrap";

const LoginPage: React.FC = () => {
    const [state, setState] = useState({});

    const handleChange = (e : FormEvent<HTMLInputElement>) => {
        const newState: any = state;
        newState[e.currentTarget.name] = e.currentTarget.value;
    };

    return AuthManager.userSession ? (
        <Redirect to={buildUrl(routes.home)} />
    ) : (
        <Container>
            <Card>
                <CardBody>
                    <h1>Login</h1>
                    <Label for="login">Login</Label>
                    <Input id="login" name="login" />
                    <Label for="password">Password</Label>
                    <Input id="password" name="password" />
                    <Button color="primary" onClick={() => AuthManager.login(buildUrl(routes.home))}>Login</Button>
                </CardBody>
            </Card>
        </Container>
    );
};

export default LoginPage;