import axios from 'axios';
import { useFetchData } from 'hooks/useFetchData';
import {
  Dispatch,
  ReactNode,
  SetStateAction,
  useEffect,
  useState,
} from 'react';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import {
  formatDate,
  millisToMinutesAndSeconds,
} from 'util/helpers/date-formaters';
import { IFilesCard } from 'types/card-data';
import { Image } from 'antd';
import styled from 'styled-components';
import { usePageContext } from 'context/PageContext';
import PList from 'components/list/List';

interface ISelectProps {
  fileName: string;
  fileId: string;
  uploadTime: string;
}

export const CreateImageAsparameter = (
  data: IFilesCard[],
  imageParameters: string[],
  setImageParameters: Dispatch<SetStateAction<string[]>>,
): ReactNode => {
  const aa: IFilesCard[] = data;
  return <PList data={data} setImageParameters={setImageParameters} />;
};
