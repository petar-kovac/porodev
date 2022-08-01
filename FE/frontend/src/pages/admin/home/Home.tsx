import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import styled from 'styled-components';

import {
  findNumberOfUploadedFiles,
  findNumberOfDeletedFiles,
  findNumberOfUsers,
  findTotalMemory,
  findTotalDownload,
} from 'service/files/files';

import DashboardCard from 'components/cards/dashboard/DashboardCard';
import StackedArea from 'components/dashboard/StackedArea';
import ColumnChart from 'components/dashboard/ColumnChart';
import { totalUploadedFiles, totalUsers } from 'service/dashboard/dashboard';
import useAdminsData from '../admins/hooks/useAdminsData';
import {
  StyledChartsContainer,
  StyledDashboardCardContainer,
  StyledHome,
} from './home-styled';

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
  const [dashboardData, setDashboardData] = useState<any>();
  const [isLoading, setIsLoading] = useState<boolean>();

  const [filesData, setFilesData] = useState<any>([]);

  useEffect(() => {
    const findFiles = async () => {
      const [firstRes, secondRes, thirdRes, fourthRes, fifthRes] =
        await Promise.all([
          findNumberOfUploadedFiles(),
          findNumberOfDeletedFiles(),
          findNumberOfUsers(),
          findTotalMemory(3),
          findTotalDownload(),
        ]);

      setFilesData([firstRes, secondRes, thirdRes, fourthRes, fifthRes]);
    };

    findFiles();
  }, []);

  console.log(filesData);

  const { findData, data } = useAdminsData();

  return (
    <StyledHome>
      <StyledDashboardCardContainer>
        <DashboardCard
          // title={value?.title}
          numberOfUploadedFiles={filesData?.[0]?.numberOfUploadedFiles}
          numberOfDeletedFiles={filesData?.[1]?.numberOfDeletedFiles}
          numberOfUsers={filesData?.[2]?.numberOfUsers}
        />
      </StyledDashboardCardContainer>
      <StyledChartsContainer>
        <StackedArea data={stackedAreaData} />
        <ColumnChart columnData={columnChartData} />
      </StyledChartsContainer>
    </StyledHome>
  );
};

export default Home;
