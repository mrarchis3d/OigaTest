import { AppBar, Toolbar, Typography } from '@mui/material';
import React from 'react';
export interface NavbarInterface {}

const Navbar : React.FC<NavbarInterface> = () => {
	return  (
	<AppBar position="fixed">
		<Toolbar>
		<Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
			Oiga Test Made with React + .Net
		</Typography>
		</Toolbar>
	</AppBar>
	);
};

export default Navbar;
