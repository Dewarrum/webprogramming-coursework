import React, {FormEvent, useState} from "react";
import AuthManager from "../Authorization/AuthManager";
import { Redirect, useHistory } from "react-router-dom";
import {buildUrl, routes} from "../../Routes/Routes";
import {Button, Card, CardBody, Container, Input, Label} from "reactstrap";

const LoginPage: React.FC = () => {
    const [state, setState] = useState({});
    const history = useHistory();

    const handleChange = (e : FormEvent<HTMLInputElement>) => {
        const newState: any = state;
        newState[e.currentTarget.name] = e.currentTarget.value;

        setState(newState);
    };

    const onAuth = (token: string | null, succeeded: boolean) => {
        history.push("/");
    };

    return AuthManager.userSession ? (
        <Redirect to={buildUrl(routes.home)} />
    ) : (
        <Container>
            <Card>
                <CardBody>
                    <h1>Login</h1>
                    <Label for="login">Login</Label>
                    <Input id="login" name="login" onChange={handleChange}/>
                    <Label for="password">Password</Label>
                    <Input id="password" name="password" onChange={handleChange}/>
                    <Button color="primary" onClick={() => AuthManager.login(state, onAuth)}>Login</Button>
                </CardBody>
            </Card>
        </Container>
    );
};

export default LoginPage;
