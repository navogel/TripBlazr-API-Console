import React, { Component } from 'react';
import { createAuthHeaders } from '../API/userManager';

class Home extends Component {
    state = {
        values: []
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
    }

    render() {
        return (
            <>
                <h1>Welcome to my app</h1>
                <ul>
                    {this.state.values.map(value => (
                        <li>{value}</li>
                    ))}
                </ul>
            </>
        );
    }
}

export default Home;
