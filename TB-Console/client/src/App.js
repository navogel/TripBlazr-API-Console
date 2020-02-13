import React, { Component } from 'react';
import { Redirect, BrowserRouter as Router, Route } from 'react-router-dom';
import Header from './components/Header';
import Login from './components/Login';
import Register from './components/Register';
import Home from './components/Home';
import { getUser, removeUser } from './API/userManager';
import ApplicationViews from './components/ApplicationViews';
import './App.css';
import './TB-Console.css';
import ScrollToTop from 'react-router-scroll-top';

class App extends Component {
    state = {
        user: getUser()
    };

    logout = () => {
        this.setState({ user: null });
        removeUser();
    };

    render() {
        return (
            <div className='App'>
                <Router>
                    <ScrollToTop>
                        <Header user={this.state.user} logout={this.logout} />
                        <Route
                            exact
                            path='/login'
                            render={() => (
                                <Login
                                    onLogin={user => this.setState({ user })}
                                />
                            )}
                        />
                        <Route
                            exact
                            path='/register'
                            render={() => (
                                <Register
                                    onLogin={user => this.setState({ user })}
                                />
                            )}
                        />
                        <Route
                            // exact
                            path='/'
                            render={() => {
                                return this.state.user ? (
                                    <ApplicationViews user={this.state.user} />
                                ) : (
                                    <Redirect to='/login' />
                                );
                            }}
                        />
                    </ScrollToTop>
                </Router>
            </div>
        );
    }
}

export default App;
