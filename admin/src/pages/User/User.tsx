import { FormLabel } from '@mui/material';
import { Container } from '@mui/system';
import React, { useEffect, useState } from 'react';
import {useParams} from 'react-router'
import { UserEntity } from '../../models/User/UserEntity';
import { GetUserById } from '../../services';
export interface UserInterface {}

const User : React.FC<UserInterface> = () => {

	let params = useParams();
	const [userInfo, setUserInfo] = useState<UserEntity>()

	useEffect(()=>{
		async function fetchData() {
			if(params!=undefined && params)
			setUserInfo(await GetUserById(Number(params.id)));
		}
		fetchData();
	},[]);

	return(
	<Container>
		<label><strong>User Information:</strong> <br /><br /></label>
		{userInfo?.firstName && <label>First Name: {userInfo?.firstName}<br /></label>}
		{userInfo?.lastName && <label>Last Name: {userInfo?.lastName}<br /></label>}
		{userInfo?.userName && <label>User Name: {userInfo?.userName}<br /></label>}
		{userInfo?.dateCreated && <label>Date: {userInfo?.dateCreated.toString()}<br /></label>}
	</Container>
	);
};

export default User;
