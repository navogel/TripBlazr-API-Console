import React from 'react';
import FormGroup from '@material-ui/core/FormGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Switch from '@material-ui/core/Switch';

export default function SwitchView(props) {
    const [state, setState] = React.useState({
        checkedA: true,
        checkedB: true
    });

    const handleChange = name => event => {
        setState({ ...state, [name]: event.target.checked });
        //pass the function from AnimalList that changes the state of the view
        props.changeView();
    };

    return (
        <FormGroup row>
            <FormControlLabel
                control={
                    <Switch
                        checked={state.checkedB}
                        onChange={handleChange('checkedB')}
                        value='checkedB'
                        color='primary'
                    />
                }
                label='Toggle View'
            />
        </FormGroup>
    );
}
