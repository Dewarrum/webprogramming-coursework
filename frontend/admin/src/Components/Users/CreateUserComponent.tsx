import React from "react";
import {Button, Form, FormGroup, Input, Label} from "reactstrap";
import { toast } from 'react-toastify';
import SingleImageUploadComponent from "../ImageUploading/SingleImageUploadComponent";

const CreateUserComponent = () => {
    const onFormSubmit = () => {
        toast.success("Success!", {
            position: toast.POSITION.TOP_RIGHT
        });
    };

    return (
        <div className="row">
            <h2 className="col-12">Create user</h2>
            <div className="col-6" style={{maxHeight: "400px"}}>
                <SingleImageUploadComponent/>
            </div>
            <Form className="col-6">
                <FormGroup>
                    <Label for="login">Login</Label>
                    <Input type="text" id="login" />
                </FormGroup>
                <FormGroup>
                    <Label for="displayName">Display Name</Label>
                    <Input type="text" id="displayName" />
                </FormGroup>
                <FormGroup>
                    <Label for="email">Email</Label>
                    <Input type="email" id="email" />
                </FormGroup>
                <FormGroup>
                    <Label for="password">Password</Label>
                    <Input type="password" id="password" />
                </FormGroup>
                <FormGroup>
                    <Button color="primary" className="float-right" onClick={onFormSubmit}>Register</Button>
                </FormGroup>
            </Form>
        </div>
    )
};

export default CreateUserComponent;
