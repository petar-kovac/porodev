import { FC, useState, useEffect } from 'react';
import ReactDOM from 'react-dom';
import { Area } from '@ant-design/plots';

interface IDashboardProps {
  data: object[];
}

const StackedArea: FC<IDashboardProps> = ({ data }) => {
  console.log(data);
  const config = {
    data,
    xField: 'date',
    yField: 'value',
    seriesField: 'department',
  };

  return <Area {...config} />;
};

export default StackedArea;
