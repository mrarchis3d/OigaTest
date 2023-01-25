import { CircularProgress, FormLabel, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField } from '@mui/material';
import { executionAsyncResource } from 'async_hooks';
import React, { useCallback, useEffect, useReducer, useRef, useState } from 'react';
import { TableWithInfiniteScroll } from '../../components/TableWithInfiniteScroll';
import { User } from '../../models/User/User';
import { UserPaged } from '../../models/User/UserPaged';
import { PageResult } from '../../models/Utils/PageResult';
import { GetFilteredUsers } from '../../services';
import { LayoutContainer } from '../../styled-components';



interface State {
	count: number;
  }
  
  export interface Action {
	type: 'increment' | 'initialState';
  }
  
  const initialState: State = { count: 1 };
  
  const reducer = (state: State, action: Action): State => {
	switch (action.type) {
	  case 'increment':
		return { count: state.count + 1 };
	  case 'initialState':
		return { count: 1 };
	  default:
		return state;
	}
  };
const Home : React.FC = () => {
	const [currentPage, setCurrentPage] = useState(1);
	const [searchValue, setSearchValue] = useState('');
  	const [data, setData] = useState<User[]>([]);
  	const [hasMore, setHasMore] = useState(true);
  	const [isLoading, setIsLoading] = useState(false);
  	const [count, setCount] = useState(0);
  	const pageSize = 10;

	useEffect(() => {
		setIsLoading(true);
		fetchData(true, currentPage, searchValue);
	  }, [currentPage]);

	
	const handleChange = async (event: React.ChangeEvent<HTMLInputElement>)  => {
		setSearchValue(event.target.value);
		setCurrentPage(1);
	};
	const getNewRows = async (pageNumber: number, inputValue?: string) : Promise<PageResult<User>> =>  {
		let page: UserPaged = {
			pageNumber: pageNumber,
			pageSize: pageSize,
			searchWords: inputValue
		};
		return GetFilteredUsers(page);
	}
	
	function fetchData(addNewRows:boolean, pageNumber: number, searchValue: string) {
		getNewRows(pageNumber, searchValue)
		  .then((response) => {
			if(addNewRows)
				setData(data.concat(response.results));
			else
				setData(response.results);
			setHasMore(response.results.length <= pageSize);
			setCount(response.total);
			setIsLoading(false);
		  })
		  .catch((error) => {
			console.error(error);
			setIsLoading(false);
		  });
	}

	useEffect(() => {
		setCurrentPage(1);
		fetchData(false, 1, searchValue);
	  }, [searchValue]
	);


	const handleScroll = (event: any) => {
		if (!isLoading && hasMore) {
		  if (event.target.offsetHeight + event.target.scrollTop+1 >= event.target.scrollHeight) {
			setCurrentPage(currentPage + 1);
		  }
		}
	  };
	  
	return (
		<>
			<TextField id="outlined-basic" label="Filter Search" variant="outlined" style={{  width: '500px' }} onChange={handleChange}/>
			<br></br>
			<LayoutContainer>
				
			<TableWithInfiniteScroll data={data}
					currentPage={currentPage}
					pageSize={pageSize}
					hasMore={hasMore}
					onScroll={handleScroll}
					isLoading={isLoading} />
			</LayoutContainer>
			<br></br>
			<FormLabel>Pages:{currentPage} ||  Showed:{data.length}  ||  Total Count:{count}</FormLabel>
		</>
	);
};

export default Home;
