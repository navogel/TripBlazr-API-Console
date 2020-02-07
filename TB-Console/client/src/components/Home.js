import React, { Component } from 'react';
import { createAuthHeaders } from '../API/userManager';
import AccountManager from '../API/accountManager';

class Home extends Component {
    state = {
        values: [],
        accounts: []
    };

    componentDidMount() {
        //creat auth header for every request
        const authHeader = createAuthHeaders();
        //ralative path
        fetch('/api/v1/values', {
            headers: authHeader
        })
            .then(response => response.json())
            .then(values => {
                this.setState({ values: values });
            });
        AccountManager.getAllAccounts().then(data => {
            this.setState({ accounts: data });
            console.log(data);
        });
    }

    render() {
        return (
            <>
                <h1>Welcome to my app</h1>
                <ul>
                    {this.state.accounts.map(account => (
                        <li>{account.city}</li>
                    ))}
                </ul>
            </>
        );
    }
}

export default Home;
