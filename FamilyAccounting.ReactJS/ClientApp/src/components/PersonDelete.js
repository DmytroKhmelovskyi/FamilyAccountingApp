import React from 'react';
import { API_BASE_URL } from '../config';
import { Container, Col, Form, Row, FormGroup, Label, Input, Button } from 'reactstrap';
import axios from 'axios'

export class PersonDelete extends React.Component {
    constructor(props) {
        super(props)

        this.onChangeFirstName = this.onChangeFirstName.bind(this);
        this.onChangeLastName = this.onChangeLastName.bind(this);
        this.onChangeEmail = this.onChangeEmail.bind(this);
        this.onChangePhone = this.onChangePhone.bind(this);
        this.onSubmit = this.onSubmit.bind(this);

        this.state = {
            FirstName: '',
            LastName: '',
            Email: '',
            Phone: ''

        }
    }

    componentDidMount() {
        axios.get(`${API_BASE_URL}/persons/details/` + this.props.match.params.personid)
            .then(response => {
                this.setState({
                    FirstName: response.data.firstName,
                    LastName: response.data.lastName,
                    Email: response.data.email,
                    Phone: response.data.phone
                });

            })
            .catch(function (error) {
                console.log(error);
            })
    }

    onChangeFirstName(e) {
        this.setState({
            FirstName: e.target.value
        });
    }
    onChangeLastName(e) {
        this.setState({
            LastName: e.target.value
        });
    }
    onChangeEmail(e) {
        this.setState({
            Email: e.target.value
        });
    }
    onChangePhone(e) {
        this.setState({
            Phone: e.target.value
        });
    }

    onSubmit(e) {
        debugger;
        e.preventDefault();
        const obj = {
            Id: this.props.match.params.personid,
            FirstName: this.state.FirstName,
            LastName: this.state.LastName,
            Email: this.state.Email,
            Address: this.state.Address

        };
        axios.delete(`${API_BASE_URL}/persons/delete/` + this.props.match.params.personid)
            .then(res => console.log(res.data));
        debugger;
        this.props.history.push('/persons')
    }
    render() {
        return (
            <Container className="App">

                <h4 className="PageHeading">Update</h4>
                <Form className="form" onSubmit={this.onSubmit}>
                    <Col>
                        <FormGroup row>
                            <Label for="FirstName" sm={2}>FirstName</Label>
                            <Col sm={10}>
                                <Input type="text" name="FirstName" value={this.state.FirstName} onChange={this.onChangeFirstName}
                                    placeholder="Enter FirstName" />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="LastName" sm={2}>LastName</Label>
                            <Col sm={10}>
                                <Input type="text" name="LastName" value={this.state.LastName} onChange={this.onChangeLastName} placeholder="Enter LastName" />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="Email" sm={2}>Email</Label>
                            <Col sm={10}>
                                <Input type="text" name="Email" value={this.state.Email} onChange={this.onChangeEmail} placeholder="Enter Email" />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="Phone" sm={2}>Phone</Label>
                            <Col sm={10}>
                                <Input type="text" name="Phone" value={this.state.Phone} onChange={this.onChangePhone} placeholder="Enter Phone" />
                            </Col>
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup row>
                            <Col sm={5}>
                            </Col>
                            <Col sm={1}>
                                <Button type="submit" color="success">Submit</Button>{' '}
                            </Col>
                            <Col sm={1}>
                                <Button color="danger">Cancel</Button>{' '}
                            </Col>
                            <Col sm={5}>
                            </Col>
                        </FormGroup>
                    </Col>
                </Form>
            </Container>
        );
    }

}

