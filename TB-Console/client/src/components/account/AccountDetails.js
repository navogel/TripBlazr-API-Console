import React, { Component } from 'react';
import Mapper from '../map/AccountMapper';

class AccountDetails extends Component {
    state = {
        address: '',
        tempAddress: ''
    };

    handleFieldChange = evt => {
        const stateToChange = {};
        stateToChange[evt.target.id] = evt.target.value;
        this.setState(stateToChange);
    };

    submitAddress = () => {
        //console.log(value);
        this.setState({
            address: this.state.tempAddress
        });
    };

    componentDidMount() {
        console.log('im account deets page', this.props);
    }

    render() {
        return (
            <>
                <h1>Welcome to my app</h1>
                <form className='modalContainer'>
                    <fieldset>
                        <div className='formgrid'>
                            <input
                                type='text'
                                required
                                onChange={this.handleFieldChange}
                                id='tempAddress'
                                placeholder='tempAddress'
                            />
                            <label htmlFor='Name'>Name</label>

                            <button
                                type='button'
                                //disabled={this.state.loadingStatus}
                                onClick={this.submitAddress}
                            >
                                Submit
                            </button>
                        </div>
                    </fieldset>
                </form>
                <ul>
                    {this.state.locations.map(account => (
                        <div key={account.accountId}>
                            <li>{account.city}</li>
                            <div className={'mapWrapper'}>
                                <Mapper
                                    className={'map'}
                                    latitude={account.latitude}
                                    longitude={account.longitude}
                                    address={this.state.address}
                                />
                            </div>
                        </div>
                    ))}
                </ul>
            </>
        );
    }
}

export default AccountDetails;
