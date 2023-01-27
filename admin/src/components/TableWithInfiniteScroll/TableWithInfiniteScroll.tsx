import { CircularProgress, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import React, { useCallback, useEffect, useLayoutEffect, useRef, useState } from 'react';
import { Link } from 'react-router-dom';
import { User } from '../../models/User/User';



interface TableWithInfiniteScrollProps {
	data: User[];
	currentPage: number;
	pageSize: number;
	onScroll: (event: any) => void;
	isLoading: boolean;
}
 
const TableWithInfiniteScroll : React.FC<TableWithInfiniteScrollProps> = ({ data, currentPage, pageSize, onScroll, isLoading }) => {
	const [visibleData, setVisibleData] = useState(data.slice(0, (currentPage + 1) * pageSize));
  
	useEffect(() => {
	  setVisibleData(data.slice(currentPage * pageSize, (currentPage + 1) * pageSize));
	}, [currentPage, data, pageSize]);
  
	useEffect(() => {
		setVisibleData(data.slice(0, (currentPage + 1) * pageSize));
	  }, [currentPage, data, pageSize]);

	return (
		<div onScroll={onScroll} style={{ height: '300px', overflow: 'auto' }}>
		  <Table>
			<TableBody>
			  {data.map((row: User, index: number) => (
				<TableRow key={index}>
				  <TableCell>{index+1}</TableCell>
				  <TableCell>{row.fullName}</TableCell>
				  <TableCell>{row.userName}</TableCell>
				  <TableCell><Link to={`users/${row.id}`}>See</Link></TableCell>
				</TableRow>
			  ))}
			  {isLoading?? <CircularProgress></CircularProgress>}
			</TableBody>
		  </Table>
		</div>
	);
};


export default TableWithInfiniteScroll;
