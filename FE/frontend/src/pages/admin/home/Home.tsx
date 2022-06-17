import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import styled from 'styled-components';

import DashboardCard from 'components/card/DashboardCard';
import StackedArea from 'components/dashboard/StackedArea';
import ColumnChart from 'components/dashboard/ColumnChart';
import useAdminsData from '../admins/hooks/useAdminsData';

const stackedAreaData = [
  { department: 'Department 1', date: 2010, value: 24 },
  { department: 'Department 1', date: 2011, value: 293 },
  { department: 'Department 2', date: 2012, value: 34 },
  { department: 'Department 1', date: 2013, value: 340 },
  { department: 'Department 2', date: 2014, value: 395 },
  { department: 'Department 2', date: 2015, value: 412 },
  { department: 'Department 2', date: 2016, value: 204 },
  { department: 'Department 1', date: 2017, value: 250 },
  { department: 'Department 1', date: 2018, value: 450 },
  { department: 'Department 2', date: 2019, value: 524 },
  { department: 'Department 1', date: 2020, value: 653 },
  { department: 'Department 2', date: 2021, value: 694 },
  { department: 'Department 1', date: 2022, value: 459 },
];

const columnChartData = [
  {
    day: 'Monday',
    value: 3,
  },
  {
    day: 'Tuesday',
    value: 5,
  },
  {
    day: 'Wednesday',
    value: 2,
  },
  {
    day: 'Thursday',
    value: 3,
  },
  {
    day: 'Friday',
    value: 4,
  },
];

const Home: FC = () => {
  const [dashboardData, setDashboardData] = useState<[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>();

  const { findData, data } = useAdminsData();

  useEffect(() => {
    const fetchCards = async () => {
      setIsLoading(true);
      try {
        const res = await axios.get(
          `${process.env.REACT_APP_MOCK_URL_2}/dashboard`,
        );
        setDashboardData(res.data);
      } catch (err) {
        console.log(err);
      } finally {
        setIsLoading(false);
      }
    };
    fetchCards();
    findData();
  }, []);

  return (
    <StyledHome>
      <StyledDashboardCardContainer>
        {dashboardData?.slice(0, 4).map((value: any) => (
          <DashboardCard
            title={value?.title}
            numberOfAdmins={value?.numberOfAdmins}
            numberOfFiles={value?.numberOfFiles}
          />
        ))}
      </StyledDashboardCardContainer>
      <StyledChartsContainer>
        <StackedArea data={stackedAreaData} />
        <ColumnChart columnData={columnChartData} />
      </StyledChartsContainer>
    </StyledHome>
  );
};

const StyledHome = styled.div`
  margin: 3rem;
`;

const StyledDashboardCardContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
`;

const StyledChartsContainer = styled.div`
  display: flex;
`;

export default Home;
