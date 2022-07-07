import axios from 'axios';
import { useFetchData } from 'hooks/useFetchData';
import { ReactNode, useEffect, useState } from 'react';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import {
  formatDate,
  millisToMinutesAndSeconds,
} from 'util/helpers/date-formaters';
import { IFilesCard } from 'types/card-data';
import { Image } from 'antd';

export const CreateImageAsparameter = (data: IFilesCard[]): ReactNode => {
  return <div>cedo</div>;
};
