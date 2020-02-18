import React, {useState} from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem } from 'reactstrap';
import {BrowserRouter, Link, NavLink} from 'react-router-dom';
import './NavMenu.css';

interface IState {
    collapsed: boolean;
}

export const NavMenu : React.FC = () => {
    const [state, setState] = useState({
        collapsed : true
    } as IState);

    const toggleNavbar = () => {
        setState({
            collapsed: !state.collapsed
        })
    };

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand to="/">react</NavbarBrand>
                    <NavbarToggler onClick={toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!state.collapsed} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink className="text-dark" to="/">Home</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink className="text-dark" to="/list">User list</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink className="text-dark" to="/profile">Profile</NavLink>
                            </NavItem>
                        </ul>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    )
};
