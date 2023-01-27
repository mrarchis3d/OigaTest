import { FormLabel, TextField } from '@mui/material';
import React, {  useEffect, useState } from 'react';
import { TableWithInfiniteScroll } from '../../components/TableWithInfiniteScroll';
import { User } from '../../models/User/User';
import { UserPaged } from '../../models/User/UserPaged';
import { PageResult } from '../../models/Utils/PageResult';
import { GetFilteredUsers } from '../../services';
import { LayoutContainer } from '../../styled-components';



const Home : React.FC = () => {
	const [currentPage, setCurrentPage] = useState(1);
	const [searchValue, setSearchValue] = useState('');
  	const [data, setData] = useState<User[]>([]);
  	const [hasMore, setHasMore] = useState(true);
  	const [isLoading, setIsLoading] = useState(false);
  	const [count, setCount] = useState(0);
  	const pageSize = 10;



	
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
	
	function fetchData(addNewRows:boolean, pageNumber: number, searchValue: string) : number {
		setIsLoading(true);
		getNewRows(pageNumber, searchValue)
		  .then((response) => {
			if(addNewRows){
				setData(data.concat(response.results));
			}
			else{
				setData(response.results);
			}
			setHasMore(data.length < response.total);
			setCount(response.total);
			return data.length;
			//setIsLoading(false);
		  })
		  .catch((error) => {
			console.error(error);
		  });
		return 0;
	}

	useEffect(() => {
		if(hasMore){
			fetchData(true, currentPage, searchValue);
			setIsLoading(false);
		}
	  }, [currentPage, hasMore]);

	useEffect(() => {
		setCurrentPage(1);
		setData([]);
		fetchData(false, 1, searchValue);
		setIsLoading(false);
	  }, [searchValue, hasMore]
	);


	const handleScroll = (event: any) => {

		if(data.length < count){
			let valueScroll = event.target.offsetHeight + event.target.scrollTop;
			if (hasMore) {
				if(currentPage>3){
					valueScroll++;
				}
				if (valueScroll >= event.target.scrollHeight) {
				  	setCurrentPage(currentPage + 1);
				}
			  }
		}

	  };
	  
	return (
		<>
			<TextField id="outlined-basic" label="Filter Search" variant="outlined" style={{  width: '500px' }} onChange={handleChange}/>
			<br></br>
			<br></br>
			{count==0 ? <><br></br><strong>Data Not Found</strong></>: <>
			
			<LayoutContainer>
				
				<TableWithInfiniteScroll data={data}
						currentPage={currentPage}
						pageSize={pageSize}
						onScroll={handleScroll}
						isLoading={isLoading} />
				</LayoutContainer>
				<br></br>
				
				<FormLabel>Pages:{currentPage} ||  Showed:{data.length}  ||  Total Count:{count}</FormLabel>
			
			
			</>}
			<br></br>

		</>
	);
};

export default Home;
